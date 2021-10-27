package com.ronaldo.mapper;

import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.CompanyDTO;

@Mapper
public interface CompanyMapper{
	 public CompanyDTO getCompany(int companyID);
	 public List<CompanyDTO> getCompanyList();
	 public List<CompanyDTO> getEnableCompanyList(boolean appEnable);
	 public void registCompany(CompanyDTO companyDTO) throws Exception;
	 public void deleteCompany(int companyID) throws Exception;
	 public void updateCompany(CompanyDTO companyDTO) throws Exception;
}