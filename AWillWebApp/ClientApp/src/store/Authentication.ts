const SET_LOGIN_PASSWORD = 'set_login_password'
const SET_LOGIN_USERNAME = 'set_login_username'

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
}

/*
	This is the initial state for this reducer
*/
const initialState: IAuthenticationState = {
	loggedIn: false,
	password: '',
	username: ''
}

/*
	This is a map of functions that create actions which may be processed by
	this reducer
*/
const actionCreators = {
	setLoginPassword: (newPassword: string): IAuthenticationReducerAction => ({
		payload: { newPassword },
		type: SET_LOGIN_PASSWORD
	}),
	setLoginUsername: (newUsername: string): IAuthenticationReducerAction => ({
		payload: { newUsername },
		type: SET_LOGIN_USERNAME
	})
}

const reducer = (
	state: Partial<IAuthenticationState> = initialState,
	action: IAuthenticationReducerAction
) => {
	const { payload, type } = action

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

	return state
}

export {
	actionCreators,
	reducer
}
