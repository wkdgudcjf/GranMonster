package com.ronaldo.domain;

import java.sql.Timestamp;
import java.util.List;

public class AppDTO {
    private int appID;
    private String appKey;
	private String appName;
	private int companyID;
	private String appAndroidURL;
	private String appIOSURL;
	private String appImageIconPath;
	private String appImageHBannerPath1;
	private String appImageVBannerPath1;
	private String appImageHBannerPath2;
	private String appImageVBannerPath2;
	private String appImageHBannerPath3;
	private String appImageVBannerPath3;
	private String appAndroidPackage;
	private String appIOSPackage;
	private String companyName;
	private Timestamp appDateTime;
	private boolean appEnable;
	private boolean appBillingVisible;
	private boolean appWidgetVisible;
	private List<AppEventDTO> appEventList;
	
    public List<AppEventDTO> getAppEventList() {
		return appEventList;
	}
	public void setAppEventList(List<AppEventDTO> appEventList) {
		this.appEventList = appEventList;
	}
	
	public String getAppAndroidURL() {
		return appAndroidURL;
	}
	public void setAppAndroidURL(String appAndroidURL) {
		this.appAndroidURL = appAndroidURL;
	}
	public String getAppIOSURL() {
		return appIOSURL;
	}
	public void setAppIOSURL(String appIOSURL) {
		this.appIOSURL = appIOSURL;
	}
	public String getAppAndroidPackage() {
		return appAndroidPackage;
	}
	public void setAppAndroidPackage(String appAndroidPackage) {
		this.appAndroidPackage = appAndroidPackage;
	}
	public String getAppIOSPackage() {
		return appIOSPackage;
	}
	public void setAppIOSPackage(String appIOSPackage) {
		this.appIOSPackage = appIOSPackage;
	}
	public boolean isAppBillingVisible() {
		return appBillingVisible;
	}
	public void setAppBillingVisible(boolean appBillingVisible) {
		this.appBillingVisible = appBillingVisible;
	}
	public boolean isAppWidgetVisible() {
		return appWidgetVisible;
	}
	public void setAppWidgetVisible(boolean appWidgetVisible) {
		this.appWidgetVisible = appWidgetVisible;
	}
	public String getAppKey() {
		return appKey;
	}
	public void setAppKey(String appKey) {
		this.appKey = appKey;
	}
	public boolean isAppEnable() {
		return appEnable;
	}
	public void setAppEnable(boolean appEnable) {
		this.appEnable = appEnable;
	}
	public Timestamp getAppDateTime() {
		return appDateTime;
	}
	public void setAppDateTime(Timestamp appDateTime) {
		this.appDateTime = appDateTime;
	}
	public String getCompanyName() {
		return companyName;
	}
	public void setCompanyName(String companyName) {
		this.companyName = companyName;
	}
	public int getAppID() {
		return appID;
	}
	public String getAppImageIconPath() {
		return appImageIconPath;
	}
	public void setAppImageIconPath(String appImageIconPath) {
		this.appImageIconPath = appImageIconPath;
	}
	public String getAppImageHBannerPath1() {
		return appImageHBannerPath1;
	}
	public void setAppImageHBannerPath1(String appImageHBannerPath1) {
		this.appImageHBannerPath1 = appImageHBannerPath1;
	}
	public String getAppImageVBannerPath1() {
		return appImageVBannerPath1;
	}
	public void setAppImageVBannerPath1(String appImageVBannerPath1) {
		this.appImageVBannerPath1 = appImageVBannerPath1;
	}
	public String getAppImageHBannerPath2() {
		return appImageHBannerPath2;
	}
	public void setAppImageHBannerPath2(String appImageHBannerPath2) {
		this.appImageHBannerPath2 = appImageHBannerPath2;
	}
	public String getAppImageVBannerPath2() {
		return appImageVBannerPath2;
	}
	public void setAppImageVBannerPath2(String appImageVBannerPath2) {
		this.appImageVBannerPath2 = appImageVBannerPath2;
	}
	public String getAppImageHBannerPath3() {
		return appImageHBannerPath3;
	}
	public void setAppImageHBannerPath3(String appImageHBannerPath3) {
		this.appImageHBannerPath3 = appImageHBannerPath3;
	}
	public String getAppImageVBannerPath3() {
		return appImageVBannerPath3;
	}
	public void setAppImageVBannerPath3(String appImageVBannerPath3) {
		this.appImageVBannerPath3 = appImageVBannerPath3;
	}
	public void setAppID(int appID) {
		this.appID = appID;
	}
	public String getAppName() {
		return appName;
	}
	public void setAppName(String appName) {
		this.appName = appName;
	}
	public int getCompanyID() {
		return companyID;
	}
	public void setCompanyID(int companyID) {
		this.companyID = companyID;
	}
}