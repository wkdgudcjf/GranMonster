package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.BillingVisibleEnum;

public class ReturnBillingVisibleVO {
	private BillingVisibleEnum state;

	public BillingVisibleEnum getState() {
		return state;
	}
	public void setState(BillingVisibleEnum state) {
		this.state = state;
	}
}