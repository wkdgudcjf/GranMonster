package com.ronaldo.domain;

import java.sql.Timestamp;

public class CompanyDTO {
    private int companyID;
    private String companyName;
    private Timestamp companyDateTime;
    private boolean companyEnable;
	public int getCompanyID() {
		return companyID;
	}
	public void setCompanyID(int companyID) {
		this.companyID = companyID;
	}
	public String getCompanyName() {
		return companyName;
	}
	public void setCompanyName(String companyName) {
		this.companyName = companyName;
	}
	public Timestamp getCompanyDateTime() {
		return companyDateTime;
	}
	public void setCompanyDateTime(Timestamp companyDateTime) {
		this.companyDateTime = companyDateTime;
	}
	public boolean isCompanyEnable() {
		return companyEnable;
	}
	public void setCompanyEnable(boolean companyEnable) {
		this.companyEnable = companyEnable;
	}
    
}