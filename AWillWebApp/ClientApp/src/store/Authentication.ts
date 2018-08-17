const ATTEMPT_LOGIN = 'attempt_login'
const SET_LOGIN_PASSWORD = 'set_login_password'
const SET_LOGIN_USERNAME = 'set_login_username'
const SET_USER_LOGGED_IN = 'set_user_logged_in'
const VALIDATE_LOGIN = 'validate_login'

/*
	This interface is used to represent the action this reducer is capable
	of processing
*/
interface IAuthenticationReducerAction {
	payload: any
	type: string
}

/*
	This interface is used to represent the state maintained by this reducer
*/
interface IAuthenticationState {
	loggedIn: boolean
	password: string
	username: string
	validCredentials: boolean
}

/*
	This is the initial state for this reducer
*/
const initialState: IAuthenticationState = {
	loggedIn: false,
	password: '',
	username: '',
	validCredentials: false
}

/*
	This is a map of functions that create actions which may be processed by
	this reducer
*/
const actionCreators = {
	// attemptLogin: (): IAuthenticationReducerAction => ({
	// 	payload: { },
	// 	type: ATTEMPT_LOGIN
	// }),
	setLoggedIn: (isLoggedIn: boolean): IAuthenticationReducerAction => ({
		payload: { isLoggedIn },
		type: SET_USER_LOGGED_IN
	}),
	setLoginPassword: (newPassword: string): IAuthenticationReducerAction => ({
		payload: { newPassword },
		type: SET_LOGIN_PASSWORD
	}),
	setLoginUsername: (newUsername: string): IAuthenticationReducerAction => ({
		payload: { newUsername },
		type: SET_LOGIN_USERNAME
	}),
	validateLogin: (): IAuthenticationReducerAction => ({
		payload: { },
		type: VALIDATE_LOGIN
	})
}

const reducer = (
	state: Partial<IAuthenticationState> = initialState,
	action: IAuthenticationReducerAction
) => {
	const { payload, type } = action

	// if (type === ATTEMPT_LOGIN) {
	// 	return {
	// 		...state,
	// 		password: payload.newPassword
	// 	}
	// }

	if (type === SET_LOGIN_PASSWORD) {
		return {
			...state,
			password: payload.newPassword
		}
	}

	if (type === SET_LOGIN_USERNAME) {
		return {
			...state,
			username: payload.newUsername
		}
	}

	if (type === SET_USER_LOGGED_IN) {
		return {
			...state,
			loginSuccess: payload.isLoggedIn
		}
	}

	if (type === VALIDATE_LOGIN) {
		if (!state.username) {
			return {
				...state,
				error: 'Username is required',
				validCredentials: false
			}
		}

		if (!state.password) {
			return {
				...state,
				error: 'Password is required',
				validCredentials: false
			}
		}

		return {
			...state,
			error: '',
			validCredentials: true
		}

		// return {
		// 	...state,
		// 	loginSuccess: payload.isLoggedIn
		// }
	}

	return state
}

export {
	actionCreators,
	IAuthenticationReducerAction,
	reducer
}
