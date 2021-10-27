package com.ronaldo.domain;

public class UserEventDTO {
    private int userEventID;
    private int userID;
    private int appEventID;
    private boolean userEventEnable;
	public int getUserEventID() {
		return userEventID;
	}
	public void setUserEventID(int userEventID) {
		this.userEventID = userEventID;
	}
	public int getUserID() {
		return userID;
	}
	public void setUserID(int userID) {
		this.userID = userID;
	}
	public int getAppEventID() {
		return appEventID;
	}
	public void setAppEventID(int appEventID) {
		this.appEventID = appEventID;
	}
	public boolean isUserEventEnable() {
		return userEventEnable;
	}
	public void setUserEventEnable(boolean userEventEnable) {
		this.userEventEnable = userEventEnable;
	}
	
}