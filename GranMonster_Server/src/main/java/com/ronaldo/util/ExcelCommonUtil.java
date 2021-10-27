package com.ronaldo.util;

import com.ronaldo.config.ExcelConfig;
import com.ronaldo.config.GranConfig;
import com.ronaldo.domain.AppEventDTO;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.ss.usermodel.Cell;
import org.apache.poi.ss.usermodel.CellStyle;
import org.apache.poi.ss.usermodel.CellType;
import org.apache.poi.ss.usermodel.CreationHelper;
import org.apache.poi.ss.usermodel.FillPatternType;
import org.apache.poi.ss.usermodel.Font;
import org.apache.poi.ss.usermodel.HorizontalAlignment;
import org.apache.poi.ss.usermodel.IndexedColors;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.ss.usermodel.Sheet;
import org.apache.poi.ss.usermodel.Workbook;
import org.apache.poi.xssf.streaming.SXSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

import javax.servlet.http.HttpServletResponse;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Map;

public class ExcelCommonUtil {

    private Workbook workbook;
    private Map<String, Object> model;
    private HttpServletResponse response;

    public ExcelCommonUtil(Workbook workbook, Map<String, Object> model, HttpServletResponse response) {
        this.workbook = workbook;
        this.model = model;
        this.response = response;
    }

    public void createExcel() {
        setFileName(response, mapToFileName());

        Sheet sheet = workbook.createSheet();
        sheet.setDefaultColumnWidth(30);
        
        createHead(sheet, mapToHeadList());

        createBody(sheet, mapToBodyList());
    }

    private String mapToFileName() {
        return (String) model.get(ExcelConfig.FILE_NAME);
    }

    private List<String> mapToHeadList() {
        return (List<String>) model.get(ExcelConfig.HEAD);
    }
/*
    private List<List<String>> mapToBodyList() {
        return (List<List<String>>) model.get(ExcelConfig.BODY);
    }
*/
    private List<AppEventDTO> mapToBodyList() {
        return (List<AppEventDTO>) model.get(ExcelConfig.BODY);
    }
    private void setFileName(HttpServletResponse response, String fileName) {
        response.setHeader("Content-Disposition",
                            "attachment; filename=\"" + setFileExtension(fileName) + "\"");
    }

    private String setFileExtension(String fileName) {
        if ( workbook instanceof XSSFWorkbook) {
            fileName += ".xlsx";
        }
        if ( workbook instanceof SXSSFWorkbook) {
            fileName += ".xlsx";
        }
        if ( workbook instanceof HSSFWorkbook) {
            fileName += ".xls";
        }

        return fileName;
    }

    private void createHead(Sheet sheet, List<String> headList) {
    	createHaedRow(sheet, headList, 0);
    }

    private void createBody(Sheet sheet, List<AppEventDTO> bodyList) {
        int rowSize = bodyList.size();
        for (int i = 0; i < rowSize; i++) {
            createBodyRow(sheet, bodyList.get(i), i + 1);
        }
    }

    private void createHaedRow(Sheet sheet, List<String> cellList, int rowNum) {
        int size = cellList.size();
        Row row = sheet.createRow(rowNum);
        CellStyle style = workbook.createCellStyle();

        Font font = workbook.createFont();
        style.setFillForegroundColor(IndexedColors.LIGHT_BLUE.getIndex());
        style.setFillPattern(FillPatternType.SOLID_FOREGROUND);
        style.setAlignment(HorizontalAlignment.CENTER); 
        font.setFontHeightInPoints((short)12);
        font.setFontName("Arial");
        font.setColor(IndexedColors.WHITE.getIndex());
        font.setBold(true);
        font.setItalic(false);
        style.setFont(font);
        
        for (int i = 0; i < size; i++) {
            Cell cell = row.createCell(i);
            cell.setCellStyle(style);
            cell.setCellValue(cellList.get(i));
            cell.setCellType(CellType.STRING);
        }
    }
    private void createBodyRow(Sheet sheet, AppEventDTO appEventDTO, int rowNum) {
        Row row = sheet.createRow(rowNum);
        CellStyle style = workbook.createCellStyle();
        CellStyle styleDate = workbook.createCellStyle();
        
        CreationHelper createHelper = workbook.getCreationHelper();
        styleDate.setDataFormat(createHelper.createDataFormat().getFormat("yyyy-MM-dd HH:mm"));
        
        Font font = workbook.createFont();
        font.setFontHeightInPoints((short)10);
        font.setFontName("Arial");
        
        style.setAlignment(HorizontalAlignment.CENTER); 
        style.setFont(font);
        
        styleDate.setAlignment(HorizontalAlignment.CENTER); 
        styleDate.setFont(font);

        for(int i=0;i<GranConfig.EVENT_CELL_NUMBER;i++)
        {
        	Cell cell = row.createCell(i);
        	switch(i)
        	{
        		case 0:
        			cell.setCellValue(appEventDTO.getAppEventID());
        			cell.setCellType(CellType.NUMERIC);
        			cell.setCellStyle(style);
        			break;
        		case 1:
        			cell.setCellValue(appEventDTO.isAppEventEnable()?"O":"X");
        			cell.setCellType(CellType.STRING);
        			cell.setCellStyle(style);
        			break;
        		case 2:
        			cell.setCellValue(appEventDTO.getAppEventKey());
        			cell.setCellType(CellType.STRING);
        			cell.setCellStyle(style);
        			break;
        		case 3:
        			cell.setCellValue(appEventDTO.getAppEventContent());
        			cell.setCellType(CellType.STRING);
        			cell.setCellStyle(style);
        			break;
        		case 4:
        			cell.setCellValue(appEventDTO.getAppEventCoin());
        			cell.setCellType(CellType.NUMERIC);
        			cell.setCellStyle(style);
        			break;
        		case 5:
        			Date sdate = new Date(appEventDTO.getAppEventStartTime().getTime());
        			cell.setCellValue(sdate);
        			cell.setCellStyle(styleDate);
        			break;
        		case 6:
        			Date edate = new Date(appEventDTO.getAppEventEndTime().getTime());
        			cell.setCellValue(edate);
        			cell.setCellStyle(styleDate);
        			break;
        		case 7:
        			cell.setCellValue(appEventDTO.getAppEventCount());
        			cell.setCellType(CellType.NUMERIC);
        			cell.setCellStyle(style);
        			break;
        		case 8:
        			cell.setCellValue(appEventDTO.getAppEventLimit());
        			cell.setCellType(CellType.NUMERIC);
        			cell.setCellStyle(style);
        			break;
        		case 9:
        			cell.setCellValue(appEventDTO.isAppEventOneoff()?"O":"X");
        			cell.setCellType(CellType.STRING);
        			cell.setCellStyle(style);
        			break;
        	}
        }
    }
}
