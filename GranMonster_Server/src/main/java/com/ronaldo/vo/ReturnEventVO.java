package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.EventEnum;

public class ReturnEventVO {
	private EventEnum state;
	public EventEnum getState() {
		return state;
	}
	public void setState(EventEnum state) {
		this.state = state;
	}
	
}