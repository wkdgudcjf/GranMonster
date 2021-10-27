package com.ronaldo.Controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.beans.factory.annotation.Autowired;

import com.ronaldo.config.SessionWire;
import com.ronaldo.service.ApiService;
import com.ronaldo.service.AuthUserService;
import com.ronaldo.vo.AuthUserVO;

@Controller
public class GateController {

	@Autowired
	private AuthUserService userService;
	@Autowired
	private ApiService apiService;
	@Autowired
	SessionWire sessionWire;

	@RequestMapping(value = { "/agreement" }, method = RequestMethod.GET)
	public String agreement(Model model) {
		return "agreement";
	}

	@RequestMapping(value = { "/", "/login" }, method = RequestMethod.GET)
	public String loginGet(Model model) {
		if (sessionWire.getId() != null) {
			return setRedirectAdmin(model);
		}
		return setInitLogin(model);
	}

	@RequestMapping(value = { "/login" }, method = RequestMethod.POST)
	public String loginPost(Model model, @ModelAttribute AuthUserVO authUserVO) {
		if (sessionWire.getId() != null) {
			return setRedirectAdmin(model);
		}
		if (userService.isVaild(authUserVO)) {
			sessionWire.setId(authUserVO.getId());
			return setRedirectAdmin(model);
		}
		return setFalseLogin(model);
	}

	@RequestMapping(value = { "/join" }, method = RequestMethod.GET)
	public String joinGet(Model model) {
		return setInitJoin(model);
	}

	@RequestMapping(value = { "/join" }, method = RequestMethod.POST)
	public String joinPost(Model model, @ModelAttribute AuthUserVO authUserVO) {
		if (userService.createAuthUser(authUserVO)){
			return setRedirectLogin(model);
		}
		return setFalseJoin(model);
	}

	@RequestMapping(value = { "/logout" }, method = RequestMethod.GET)
	public String logout(Model model) {
		if (sessionWire.getId() != null) {
			sessionWire.invaildate();
		}
		return setRedirectLogin(model);
	}

	@RequestMapping(value = "/admin", method = RequestMethod.GET)
	public String admin(Model model) {
		if (sessionWire.getId() == null) {
			return setRedirectLogin(model);
		}
		return setAdmin(model);
	}

	private String setInitLogin(Model model) {
		model.addAttribute("message", "그랑몬스터");
		return "login";
	}

	private String setRedirectLogin(Model model) {
		model.addAttribute("message", "그랑몬스터");
		return "redirect:/login";
	}

	private String setFalseLogin(Model model) {
		model.addAttribute("message", "아이디 비밀번호를 확인하세요");
		return "login";
	}

	private String setAdmin(Model model) {
		model.addAttribute("applist",apiService.getAppList());
		model.addAttribute("user", userService.searchAuthUser(sessionWire.getId()));
		return "admin";
	}

	private String setRedirectAdmin(Model model) {
		model.addAttribute("applist",apiService.getAppList());
		model.addAttribute("user", userService.searchAuthUser(sessionWire.getId()));
		return "redirect:/admin";
	}

	private String setFalseJoin(Model model) {
		model.addAttribute("message", "동일한 id가 존재하거나 인증번호가 틀립니다.");
		return "join";
	}

	private String setInitJoin(Model model) {
		model.addAttribute("message", "");
		return "join";
	}
}
