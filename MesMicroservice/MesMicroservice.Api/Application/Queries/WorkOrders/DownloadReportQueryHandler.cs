using OfficeOpenXml;
using Azure.Storage.Blobs;
using MesMicroservice.Api.Application.Exceptions;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;

namespace MesMicroservice.Api.Application.Queries.WorkOrders;

public class DownloadReportQueryHandler : IRequestHandler<DownloadReportQuery, byte[]>
{
    private readonly ApplicationDbContext _context;

    public DownloadReportQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> Handle(DownloadReportQuery request, CancellationToken cancellationToken)
    {
        var workOrder = await _context.WorkOrders
            .Include(x => x.ManufacturingRecords)
            .ThenInclude(x => x.Equipments)
            .Include(x => x.ManufacturingOrder)
            .ThenInclude(x => x.MaterialDefinition)
            .Include(x => x.PrerequisiteOperations)
            .Include(x => x.WorkCenter)
            .ThenInclude(x => x!.Parent)
            .ThenInclude(x => x!.Parent)
            .ThenInclude(x => x!.Parent)
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.ManufacturingOrder.ManufacturingOrderId == request.ManufacturingOrderId &&
                x.WorkOrderId == request.WorkOrderId) ?? throw new ResourceNotFoundException(nameof(WorkOrder), request.WorkOrderId);

        var manufacturingRecords = workOrder.ManufacturingRecords.ToList();
        var equipmentIds = manufacturingRecords.First().Equipments.Select(x => x.ResourceId).Distinct().ToList();
        var equipmentIdsString = string.Join(",", equipmentIds);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=wembleystorage;AccountKey=kmiMHp/AZez/JgT0kIRSEsoeV+qJx35ejvrz6r8IjUi4qOKppsKCSb69LrjZAhjKsCqFe6MIZnsY+AStxggfOA==;EndpointSuffix=core.windows.net");
        var containerClient = blobServiceClient.GetBlobContainerClient("wembley");
        var blobClient = containerClient.GetBlobClient("RecordReport-Wembley.xlsx");

        var stream = await blobClient.OpenReadAsync();
        var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets["sheet1"];

        worksheet.Cells["A6"].Value = $"MÃ ĐƠN SẢN XUẤT: {workOrder.ManufacturingOrder.ManufacturingOrderId}";
        worksheet.Cells["C6"].Value = $"MÃ CÔNG ĐOẠN: {workOrder.WorkOrderId}";
        worksheet.Cells["A7"].Value = $"MÃ MÁY: {equipmentIdsString}";
        worksheet.Cells["C7"].Value = $"THỜI ĐIỂM BẮT ĐẦU ĐƠN: {workOrder.ActuallyStartTime:dd/MM/yyyy HH:mm:ss}";
        worksheet.Cells["E7"].Value = $"THỜI ĐIỂM KẾT THÚC ĐƠN: {workOrder.ActuallyEndTime:dd/MM/yyyy HH:mm:ss}";
        worksheet.Cells["A8"].Value = $"MÃ SẢN PHẨM: {workOrder.ManufacturingOrder.MaterialDefinition.ResourceId}";
        worksheet.Cells["C8"].Value = $"TÊN SẢN PHẨM: {workOrder.ManufacturingOrder.MaterialDefinition.Name}";
        worksheet.Cells["E8"].Value = $"SỐ LƯỢNG SẢN XUẤT: {(int)workOrder.ActualQuantity}";
        worksheet.Cells["A6:G8"].Style.Font.Bold = true;

        worksheet.Cells["G2:G3"].Value = DateTime.Now.Date;

        for (int column = 2; column <= 5; column++)
        {
            int row = 11;
            foreach (var manufacturingRecord in manufacturingRecords)
            {
                var cell = worksheet.Cells[row, column];
                switch (column)
                {
                    case 2:
                        cell.Value = manufacturingRecord.StartTime;
                        break;
                    case 3:
                        cell.Value = manufacturingRecord.EndTime;
                        break;
                    case 4:
                        cell.Value = manufacturingRecord.Output;
                        break;
                    case 5:
                        cell.Value = manufacturingRecord.Defects;
                        break;
                }
                row++;
            }
        }

        var streamModified = new MemoryStream();
        package.SaveAs(streamModified);

        byte[] file = streamModified.ToArray();

        return file;
    }
}
