package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.ExhaustEnum;

public class ReturnExhaustVO {
	private int coin;
	private ExhaustEnum state;
	public int getCoin() {
		return coin;
	}
	public void setCoin(int coin) {
		this.coin = coin;
	}
	public ExhaustEnum getState() {
		return state;
	}
	public void setState(ExhaustEnum state) {
		this.state = state;
	}
	
}