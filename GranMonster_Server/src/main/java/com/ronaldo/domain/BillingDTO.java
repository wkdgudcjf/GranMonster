package com.ronaldo.domain;

import java.sql.Timestamp;

public class BillingDTO {
    private int billingID;
    private int userID;
    private int appID;
    private int billingMoney;
    private int billingCoin;
    private String billingType;
    private String appName;
    private String userKey;
	private Timestamp billingDateTime;
	private boolean appType; // true == Android , false == IOS
	private String appTypeString;
	
	public String getAppTypeString() {
		return appTypeString;
	}
	public void setAppTypeString(String appTypeString) {
		this.appTypeString = appTypeString;
	}
	public boolean isAppType() {
		return appType;
	}
	public void setAppType(boolean appType) {
		this.appType = appType;
	}
	public String getBillingType() {
		return billingType;
	}
	public void setBillingType(String billingType) {
		this.billingType = billingType;
	}
	public String getUserKey() {
		return userKey;
	}
	public void setUserKey(String userKey) {
		this.userKey = userKey;
	}
	public int getUserID() {
		return userID;
	}
	public void setUserID(int userID) {
		this.userID = userID;
	}
	public int getAppID() {
		return appID;
	}
	public void setAppID(int appID) {
		this.appID = appID;
	}
	public int getBillingID() {
		return billingID;
	}
	public void setBillingID(int billingID) {
		this.billingID = billingID;
	}
	
	public int getBillingMoney() {
		return billingMoney;
	}
	public void setBillingMoney(int billingMoney) {
		this.billingMoney = billingMoney;
	}
	public int getBillingCoin() {
		return billingCoin;
	}
	public void setBillingCoin(int billingCoin) {
		this.billingCoin = billingCoin;
	}
	
	public String getAppName() {
		return appName;
	}
	public void setAppName(String appName) {
		this.appName = appName;
	}
	public Timestamp getBillingDateTime() {
		return billingDateTime;
	}
	public void setBillingDateTime(Timestamp billingDateTime) {
		this.billingDateTime = billingDateTime;
	}
}