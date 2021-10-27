package com.ronaldo.vo;

import java.util.List;

public class ReturnAppVO {
	private String appName;
	private String appKey;
	private String appURL;
	private String appImageIconPath;
	private String appImageHBannerPath1;
	private String appImageVBannerPath1;
	private String appImageHBannerPath2;
	private String appImageVBannerPath2;
	private String appImageHBannerPath3;
	private String appImageVBannerPath3;
	private String appPackage;
	private boolean appInstall;
	private List<ReturnAppEventVO> appEventList;
	
	public String getAppKey() {
		return appKey;
	}
	public void setAppKey(String appKey) {
		this.appKey = appKey;
	}
	public String getAppName() {
		return appName;
	}
	public void setAppName(String appName) {
		this.appName = appName;
	}
	public String getAppURL() {
		return appURL;
	}
	public void setAppURL(String appURL) {
		this.appURL = appURL;
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
	public String getAppPackage() {
		return appPackage;
	}
	public void setAppPackage(String appPackage) {
		this.appPackage = appPackage;
	}
	
	public boolean isAppInstall() {
		return appInstall;
	}
	public void setAppInstall(boolean appInstall) {
		this.appInstall = appInstall;
	}
	public List<ReturnAppEventVO> getAppEventList() {
		return appEventList;
	}
	public void setAppEventList(List<ReturnAppEventVO> appEventList) {
		this.appEventList = appEventList;
	}
	
}