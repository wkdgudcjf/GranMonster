package com.ronaldo.vo;

import org.apache.poi.ss.usermodel.Row;

import java.io.Serializable;
import java.util.Date;

public class ReceviceAppEventExcelVO implements Serializable {
	private int appEventID;
	private String appEventEnable;
	private String appEventKey;
    private String appEventContent;
	private int appEventCoin;
	private Date appEventStartTime;
	private Date appEventEndTime;
	private int appEventCount;
	private int appEventLimit;
	private String appEventOffsets;
	public static ReceviceAppEventExcelVO rowOf(Row row) {
		ReceviceAppEventExcelVO product = new ReceviceAppEventExcelVO();
		product.setAppEventID((int)row.getCell(0).getNumericCellValue());
		product.setAppEventEnable(row.getCell(1).getStringCellValue());
		product.setAppEventKey(row.getCell(2).getStringCellValue());
        product.setAppEventContent(row.getCell(3).getStringCellValue());
        product.setAppEventCoin((int)row.getCell(4).getNumericCellValue());
        product.setAppEventStartTime(row.getCell(5).getDateCellValue());
        product.setAppEventEndTime(row.getCell(6).getDateCellValue());
        product.setAppEventCount((int)row.getCell(7).getNumericCellValue());
        product.setAppEventLimit((int)row.getCell(8).getNumericCellValue());
        product.setAppEventOffsets(row.getCell(9).getStringCellValue());
        return product;
    }

	public String getAppEventOffsets() {
		return appEventOffsets;
	}

	public void setAppEventOffsets(String appEventOffsets) {
		this.appEventOffsets = appEventOffsets;
	}

	public int getAppEventID() {
		return appEventID;
	}

	public void setAppEventID(int appEventID) {
		this.appEventID = appEventID;
	}

	public String getAppEventEnable() {
		return appEventEnable;
	}

	public void setAppEventEnable(String appEventEnable) {
		this.appEventEnable = appEventEnable;
	}

	public String getAppEventKey() {
		return appEventKey;
	}

	public void setAppEventKey(String appEventKey) {
		this.appEventKey = appEventKey;
	}

	public String getAppEventContent() {
		return appEventContent;
	}

	public void setAppEventContent(String appEventContent) {
		this.appEventContent = appEventContent;
	}

	public int getAppEventCoin() {
		return appEventCoin;
	}

	public void setAppEventCoin(int appEventCoin) {
		this.appEventCoin = appEventCoin;
	}

	public Date getAppEventStartTime() {
		return appEventStartTime;
	}

	public void setAppEventStartTime(Date appEventStartTime) {
		this.appEventStartTime = appEventStartTime;
	}

	public Date getAppEventEndTime() {
		return appEventEndTime;
	}

	public void setAppEventEndTime(Date appEventEndTime) {
		this.appEventEndTime = appEventEndTime;
	}

	public int getAppEventCount() {
		return appEventCount;
	}

	public void setAppEventCount(int appEventCount) {
		this.appEventCount = appEventCount;
	}

	public int getAppEventLimit() {
		return appEventLimit;
	}

	public void setAppEventLimit(int appEventLimit) {
		this.appEventLimit = appEventLimit;
	}
	
}
