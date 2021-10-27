package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.PlayEnum;

public class ReturnPlayVO {
	private PlayEnum state;

	public PlayEnum getState() {
		return state;
	}
	public void setState(PlayEnum state) {
		this.state = state;
	}
}