import { History } from 'history'

import { userService } from '../services/UserService'

const alertConstants = {
	CLEAR: 'ALERT_CLEAR',
	ERROR: 'ALERT_ERROR',
	SUCCESS: 'ALERT_SUCCESS'
}

const alertActions = {
	clear: () => ({ type: alertConstants.CLEAR }),
	error: (message: any) => ({ type: alertConstants.ERROR, message }),
	success: (message: any) => ({ type: alertConstants.SUCCESS, message })
}

// const deleteUser = (id: any) => {
// 	return (dispatch: any) => {
// 		dispatch(request(id))

// 		userService
// 			.deleteUser(id)
// 			.then(
// 				(user: any) => dispatch(success(id)),
// 				(error: any) => dispatch(failure(id, error.toString()))
// 			)
// 	}

// 	const request = (requestId: any) => {
// 		return { type: userConstants.DELETE_REQUEST, requestId }
// 	}
// 	const success = (successId: any) => {
// 		return { type: userConstants.DELETE_SUCCESS, successId }
// 	}
// 	const failure = (failureId: any, error: any) => {
// 		return { type: userConstants.DELETE_FAILURE, failureId, error }
// 	}
// }

// const getAll = () => {
// 	return (dispatch: any) => {
// 		dispatch(request())

// 		userService
// 			.getAll()
// 			.then(
// 				(users: any) => dispatch(success(users)),
// 				(error: any) => dispatch(failure(error.toString()))
// 			)
// 	}

// 	const request = () => {
// 		return { type: userConstants.GETALL_REQUEST }
// 	}
// 	const success = (users: any) => {
// 		return { type: userConstants.GETALL_SUCCESS, users }
// 	}
// 	const failure = (error: any) => {
// 		return { type: userConstants.GETALL_FAILURE, error }
// 	}
// }

const login = (username: string, password: string): ((dispatch: any) => Promise<boolean>) => {
	const failure = (error: any) => {
		return { type: userConstants.LOGIN_FAILURE, error }
	}
	const request = (user: any) => {
		return { type: userConstants.LOGIN_REQUEST, user }
	}
	const success = (user: any) => {
		return { type: userConstants.LOGIN_SUCCESS, user }
	}

	return (dispatch: any): Promise<boolean> => {
		dispatch(request({ username }))
		return userService
			.login(username, password)(dispatch)
			.then(
				(wasSuccessful: boolean) => {
					dispatch(success(wasSuccessful))
					// history.push('/')
					return new Promise<boolean>((resolve, reject) => resolve(wasSuccessful))
				},
				(error: any) => {
					console.log(
						`[login | UserActions | callback | error] error = ${JSON.stringify(
							error
						)}`
					)
					dispatch(failure(error.toString()))
					dispatch(alertActions.error(error.toString()))
					return new Promise<boolean>((resolve, reject) => reject(error))
				}
			)
			.catch((err: any) => {
				console.error(`[UserActions | catch] = ${err}`)
				return new Promise<boolean>((resolve, reject) => reject(err))
			})
	}
}

const logout = () => {
	userService.logout()
	return { type: userConstants.LOGOUT }
}

// const register = (user: any, history: History) => {
// 	return (dispatch: any) => {
// 		dispatch(request(user))

// 		userService.register(user).then(
// 			(responseUser: any) => {
// 				dispatch(success(responseUser))
// 				history.push('/login')
// 				dispatch(alertActions.success('Registration successful'))
// 			},
// 			(error: any) => {
// 				dispatch(failure(error.toString()))
// 				dispatch(alertActions.error(error.toString()))
// 			}
// 		)
// 	}

// 	const request = (requestUser: any) => {
// 		return { type: userConstants.REGISTER_REQUEST, requestUser }
// 	}
// 	const success = (successUser: any) => {
// 		return { type: userConstants.REGISTER_SUCCESS, successUser }
// 	}
// 	const failure = (error: any) => {
// 		return { type: userConstants.REGISTER_FAILURE, error }
// 	}
// }

const userActions = {
	// deleteUser,
	// getAll,
	login,
	logout
	// register
}

const userConstants = {
	DELETE_FAILURE: 'USERS_DELETE_FAILURE',
	DELETE_REQUEST: 'USERS_DELETE_REQUEST',
	DELETE_SUCCESS: 'USERS_DELETE_SUCCESS',
	GETALL_FAILURE: 'USERS_GETALL_FAILURE',
	GETALL_REQUEST: 'USERS_GETALL_REQUEST',
	GETALL_SUCCESS: 'USERS_GETALL_SUCCESS',
	LOGIN_FAILURE: 'USERS_LOGIN_FAILURE',
	LOGIN_REQUEST: 'USERS_LOGIN_REQUEST',
	LOGIN_SUCCESS: 'USERS_LOGIN_SUCCESS',
	LOGOUT: 'USERS_LOGOUT',
	REGISTER_FAILURE: 'USERS_REGISTER_FAILURE',
	REGISTER_REQUEST: 'USERS_REGISTER_REQUEST',
	REGISTER_SUCCESS: 'USERS_REGISTER_SUCCESS'
}

export { userActions, userConstants }
