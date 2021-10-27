package com.ronaldo.config;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;

import com.ronaldo.service.AuthUserService;

@Configuration
public class SecurityConfig extends WebSecurityConfigurerAdapter
{
	@Autowired
	AuthUserService authUserService;
	@Override
	public void configure(WebSecurity web) throws Exception
	{
		web.ignoring().antMatchers("/resources/**", "/css/**", "/script/**", "/api/**");
	}
	@Override
	protected void configure(HttpSecurity http) throws Exception
	{
		http
		.csrf().disable()
		.authorizeRequests()
			.antMatchers("/**").permitAll();
		/*	.antMatchers("/login").permitAll()
			.antMatchers("/loginProcessing").permitAll()
			.antMatchers("/join").permitAll()
			.antMatchers("/company/**").hasAuthority("USER")
			.antMatchers("/admin/**").hasAuthority("ADMIN")
			.anyRequest().authenticated()
			.and()
		.formLogin()
			// 로그인 페이지
			.loginPage("/login")
			.usernameParameter("email")
			.passwordParameter("password")
			.and()
		.logout()
			.logoutRequestMatcher(new AntPathRequestMatcher("/logout"))
			// 로그아웃이 성공했을 경우 이동할 페이지
			.logoutSuccessUrl("/");
		*/
	}
	@Override
	protected void configure(AuthenticationManagerBuilder auth) throws Exception
	{
		//auth.jdbcAuthentication()
		//	.passwordEncoder(authUserService.passwordEncoder()) ;
	}
	@Bean
	@Override
	public AuthenticationManager authenticationManagerBean() throws Exception
	{ 
		return super.authenticationManagerBean();
	}
}
