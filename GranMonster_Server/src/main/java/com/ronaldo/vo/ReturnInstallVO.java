package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.InstallEnum;

public class ReturnInstallVO {
	private InstallEnum state;

	public InstallEnum getState() {
		return state;
	}
	public void setState(InstallEnum state) {
		this.state = state;
	}
}