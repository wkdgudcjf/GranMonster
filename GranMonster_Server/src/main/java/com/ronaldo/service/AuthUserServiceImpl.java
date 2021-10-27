package com.ronaldo.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import com.ronaldo.domain.AuthUserDTO;
import com.ronaldo.mapper.AuthUserMapper;
import com.ronaldo.vo.AuthUserVO;
@Service
public class AuthUserServiceImpl implements AuthUserService
{ 
	@Autowired 
	AuthUserMapper authUserMapper;
	private PasswordEncoder passwordEncoder = new BCryptPasswordEncoder();
	@Override
	public boolean isVaild(AuthUserVO authUserVO) {
		AuthUserDTO authUserDTO = authUserMapper.searchAuthUser(authUserVO.getId());
		if(authUserDTO==null || !(passwordEncoder.matches(authUserVO.getPassword(), authUserDTO.getAuthUserPassword())))
		{
			return false;
		}
		// TODO Auto-generated method stub
		return true;
	}
	@Override
	public AuthUserDTO searchAuthUser(String authUserID) {
		// TODO Auto-generated method stub
		return authUserMapper.searchAuthUser(authUserID);
	}
	@Override
	public boolean createAuthUser(AuthUserVO authUserVO) {
		if(!authUserVO.getInputNumberCheck().equals("ronaldo"))
		{
			return false;
		}
		if(authUserMapper.searchAuthUser(authUserVO.getId())!=null)
		{
			return false;
		}
		AuthUserDTO authUserDTO = new AuthUserDTO(authUserVO.getId(),passwordEncoder.encode(authUserVO.getPassword()),authUserVO.getEmail(),"ADMIN");
		authUserMapper.createAuthUser(authUserDTO);
		return true;
	} 
}
