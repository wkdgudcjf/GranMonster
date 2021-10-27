package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.PayloadEnum;

public class ReturnPayloadVO {
	private String userPayload;
	private PayloadEnum state;
	public String getUserPayload() {
		return userPayload;
	}
	public void setUserPayload(String userPayload) {
		this.userPayload = userPayload;
	}
	public PayloadEnum getState() {
		return state;
	}
	public void setState(PayloadEnum state) {
		this.state = state;
	}
	
}