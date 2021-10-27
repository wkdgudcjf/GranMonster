package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.PurchaseEnum;

public class ReturnPurchaseVO {
	private int coin;
	private PurchaseEnum state;
	public int getCoin() {
		return coin;
	}
	public void setCoin(int coin) {
		this.coin = coin;
	}
	public PurchaseEnum getState() {
		return state;
	}
	public void setState(PurchaseEnum state) {
		this.state = state;
	}
	
}