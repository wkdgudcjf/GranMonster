package com.ronaldo.domain;

import java.sql.Timestamp;

import com.fasterxml.jackson.databind.annotation.JsonSerialize;
import com.ronaldo.config.Write_Formatting;

public class AppEventDTO {
    private int appEventID;
    private int appID;
    private String appEventContent;
	private String appEventKey;
	private int appEventCoin;
	private Timestamp appEventStartTime;
	private Timestamp appEventEndTime;
	private int appEventLimit;
	private int appEventCount;
	private boolean appEventOneoff;
	private boolean appEventEnable;
	private String appEventOneoffs;
	private String appEventSuccessEnable;
	private String appEventRewardEnable;
	public String getAppEventOneoffs() {
		return appEventOneoffs;
	}
	public void setAppEventOneoffs(String appEventOneoffs) {
		this.appEventOneoffs = appEventOneoffs;
	}
	public boolean isAppEventOneoff() {
		return appEventOneoff;
	}
	public void setAppEventOneoff(boolean appEventOneoff) {
		this.appEventOneoff = appEventOneoff;
	}
	public String getAppEventKey() {
		return appEventKey;
	}
	public void setAppEventKey(String appEventKey) {
		this.appEventKey = appEventKey;
	}
	@JsonSerialize(using=Write_Formatting.class)
	public Timestamp getAppEventStartTime() {
		return appEventStartTime;
	}
	public void setAppEventStartTime(Timestamp appEventStartTime) {
		this.appEventStartTime = appEventStartTime;
	}
	@JsonSerialize(using=Write_Formatting.class)
	public Timestamp getAppEventEndTime() {
		return appEventEndTime;
	}
	public void setAppEventEndTime(Timestamp appEventEndTime) {
		this.appEventEndTime = appEventEndTime;
	}
	public int getAppEventLimit() {
		return appEventLimit;
	}
	public void setAppEventLimit(int appEventLimit) {
		this.appEventLimit = appEventLimit;
	}
	public int getAppEventCount() {
		return appEventCount;
	}
	public void setAppEventCount(int appEventCount) {
		this.appEventCount = appEventCount;
	}
	public int getAppEventID() {
		return appEventID;
	}
	public void setAppEventID(int appEventID) {
		this.appEventID = appEventID;
	}
	public int getAppID() {
		return appID;
	}
	public void setAppID(int appID) {
		this.appID = appID;
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
	public boolean isAppEventEnable() {
		return appEventEnable;
	}
	public void setAppEventEnable(boolean appEventEnable) {
		this.appEventEnable = appEventEnable;
	}
	public String getAppEventSuccessEnable() {
		return appEventSuccessEnable;
	}
	public void setAppEventSuccessEnable(String appEventSuccessEnable) {
		this.appEventSuccessEnable = appEventSuccessEnable;
	}
	public String getAppEventRewardEnable() {
		return appEventRewardEnable;
	}
	public void setAppEventRewardEnable(String appEventRewardEnable) {
		this.appEventRewardEnable = appEventRewardEnable;
	}
	
}