package com.ronaldo.mapper;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.AuthUserDTO;

@Mapper
public interface AuthUserMapper{
	public AuthUserDTO searchAuthUser(String authUserID); 
	//public List<GrantedAuthority> readAuthority(String username);
	public void createAuthUser(AuthUserDTO authUserDTO);
	//public void createAuthority(AuthUserVo user);
	public void deleteAuthUser(String authUserID);
	//public void deleteAuthority(String username);
}