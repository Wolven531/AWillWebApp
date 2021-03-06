// import config from 'config'
// TODO: move this to config / read from config
const APIUrl = 'https://localhost:5001'

const authHeader = () => {
	// NOTE: authorization header with jwt token
	const user = JSON.parse(String(localStorage.getItem('user')))

	if (user && user.token) {
		return { Authorization: 'Bearer ' + user.token }
	}
	return {}
}

const deleteUser = (id: any) => {
	// const requestOptions = {
	// 	headers: authHeader(),
	// 	method: 'DELETE'
	// }

	// return fetch(`${APIUrl}/users/${id}`, requestOptions as any).then(
	// 	handleResponse
	// )
}

const getAll = () => {
	// const requestOptions = {
	// 	headers: authHeader(),
	// 	method: 'GET'
	// }

	// return fetch(`${APIUrl}/users`, requestOptions as any).then(handleResponse)
}

const getById = (id: any) => {
	// const requestOptions = {
	// 	headers: authHeader(),
	// 	method: 'GET'
	// }

	// return fetch(`${APIUrl}/users/${id}`, requestOptions as any).then(
	// 	handleResponse
	// )
}

const login = (username: string, password: string): (dispatch: any) => Promise<boolean> => {
	const authenticationPostOptions = {
		body: JSON.stringify({
			password,
			username
		}),
		cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
		credentials: 'same-origin', // include, same-origin, *omit
		headers: {
			'Content-Type': 'application/json; charset=utf-8'
		},
		method: 'POST'
	}

	return (dispatch: any): Promise<boolean> => {
		return fetch('/api/auth', authenticationPostOptions as any)
		// .then(response => response.json())
		.then(handleResponse)
		.then(authenticationResult => {
			if (!authenticationResult) {
				console.log(`[login | UserService | callback] no result`)
				return new Promise<boolean>((resolve, reject) => {
					reject('[login | UserService | callback] There was no result')
				})
			}
			if (!authenticationResult.success) {
				console.log(`[login | UserService | callback] auth failed`)
				// this.setState({ error: 'Auth Failed' })
				return new Promise<boolean>((resolve, reject) => {
					resolve(false)
				})
			}
			// TODO: update this to token after backend has one
			console.warn(`[login | UserService | callback] auth succeeded, storing username for BAD auth...`)
			localStorage.setItem('user', username)
			// console.log(`[login | UserService | callback] auth succeeded, storing token...`)
			// localStorage.setItem('user', JSON.stringify(authenticationResult.token))
			return new Promise<boolean>((resolve, reject) => {
				resolve(true)
			})
		// 	this.setState({ error: '', loginSuccess: true })
		// 	window.setTimeout(() => {
		// 		window.location.reload()
		// 	}, 2000)
		})
		.catch(error => {
			console.error(`Posting Authentication Error = ${error}`)
			return new Promise<boolean>((resolve, reject) => {
				reject(`[login | UserService | callback] error = ${error}`)
			})
		})
	}
}

const logout = () => {
	console.log(`[logout | UserService] removing token...`)
	localStorage.removeItem('user')
}

const register = (user: any) => {
	// const requestOptions = {
	// 	body: JSON.stringify(user),
	// 	headers: { 'Content-Type': 'application/json' },
	// 	method: 'POST'
	// }

	// return fetch(`${APIUrl}/users/register`, requestOptions).then(
	// 	handleResponse
	// )
}

const update = (user: any) => {
	// const requestOptions = {
	// 	body: JSON.stringify(user),
	// 	headers: {
	// 		...authHeader(),
	// 		'Content-Type': 'application/json'
	// 	},
	// 	method: 'PUT'
	// }

	// return fetch(`${APIUrl}/users/${user.id}`, requestOptions as any).then(
	// 	handleResponse
	// )
}

/// This function is not exported
const handleResponse = (response: Response) => {
	return response.text().then(text => {
		const data = text && JSON.parse(text)
		if (!response.ok) {
			if (response.status === 401) {
				// NOTE: auto logout if 401 response returned from api
				logout()
				location.reload(true)
			}

			const error = (data && data.message) || response.statusText
			return Promise.reject(error)
		}

		return data
	})
}
const userService = {
	deleteUser,
	getAll,
	getById,
	login,
	logout,
	register,
	update
}

export { userService }
