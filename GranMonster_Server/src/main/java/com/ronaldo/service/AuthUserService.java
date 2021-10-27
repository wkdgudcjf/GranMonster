package com.ronaldo.service;

import com.ronaldo.domain.AuthUserDTO;
import com.ronaldo.vo.AuthUserVO;

public interface AuthUserService
{ 
	public boolean isVaild(AuthUserVO authUserVO);
	public boolean createAuthUser(AuthUserVO authUserVO);
	public AuthUserDTO searchAuthUser(String id);
}
