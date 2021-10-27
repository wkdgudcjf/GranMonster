package com.ronaldo.util;

import com.ronaldo.config.ExcelConfig;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.openxml4j.exceptions.InvalidFormatException;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.ss.usermodel.Sheet;
import org.apache.poi.ss.usermodel.Workbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class ExcelRead {

    public static <T> List<T> readExcelToList(final MultipartFile multipartFile,
                                       final Function<Row, T> rowFunc) throws IOException, InvalidFormatException {
    	
        final Workbook workbook = readWorkbook(multipartFile);
        final Sheet sheet = workbook.getSheetAt(0);
        final int rowCount = sheet.getPhysicalNumberOfRows();

        return IntStream
                .range(1, rowCount)
                .mapToObj(rowIndex -> rowFunc.apply(sheet.getRow(rowIndex)))
                .collect(Collectors.toList());
    }

    private static Workbook readWorkbook(MultipartFile multipartFile) throws IOException, InvalidFormatException {
        verifyFileExtension(multipartFile);
        return multipartFileToWorkbook(multipartFile);
    }

    private static void verifyFileExtension(MultipartFile multipartFile) throws InvalidFormatException {
        if( !isExcelExtension(multipartFile.getOriginalFilename()) ) {
            throw new InvalidFormatException("This file extension is not verify");
        }
    }

    private static boolean isExcelExtension(String fileName) {
        return fileName.endsWith(ExcelConfig.XLS) || fileName.endsWith(ExcelConfig.XLSX);
    }

    private static boolean isExcelXls(String fileName) {
        return fileName.endsWith(ExcelConfig.XLS);
    }

    private static boolean isExcelXlsx(String fileName) {
        return fileName.endsWith(ExcelConfig.XLSX);
    }

    private static Workbook multipartFileToWorkbook(MultipartFile multipartFile)
            throws IOException, InvalidFormatException {
        if( isExcelXls(multipartFile.getOriginalFilename()) ) {
            return new HSSFWorkbook(multipartFile.getInputStream());
        } else {
            return new XSSFWorkbook(multipartFile.getInputStream());
        }
    }
}