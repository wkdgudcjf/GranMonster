package com.ronaldo.config;

import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class SessionWire
{
	@Autowired
	HttpSession httpSession;
	
	private final static String ID = "ID";
	public String getId()
	{
		return (String)httpSession.getAttribute(ID);
	}
	public void invaildate()
	{
		httpSession.invalidate();
	}
	public void setId(String id)
	{
		httpSession.setAttribute(ID, id);
	}
}
