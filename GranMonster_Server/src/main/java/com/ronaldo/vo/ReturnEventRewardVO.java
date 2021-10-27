package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.EventRewardEnum;

public class ReturnEventRewardVO {
	private int coin;
	private EventRewardEnum state;
	public int getCoin() {
		return coin;
	}
	public void setCoin(int coin) {
		this.coin = coin;
	}
	public EventRewardEnum getState() {
		return state;
	}
	public void setState(EventRewardEnum state) {
		this.state = state;
	}
	
}