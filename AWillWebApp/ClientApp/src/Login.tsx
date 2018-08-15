import * as React from 'react'
import { connect } from 'react-redux'

import { actionCreators } from './store/Authentication'

import './Login.css'

const mapStateToProps = (state: any) => {
	const { error, loginSuccess, password, username } = state.authentication

	return {
		error,
		loginSuccess,
		password,
		username
	}
}

const mapDispatchToProps = (dispatch: (action: any) => any) => {
	console.log(`[mapDispatchToProps | Login]`)
	const dismissError = () => {
		// this.setState({ error: '' })
	}
	return {
		dismissError,
		handleFormSubmission: (evt: React.FormEvent<HTMLFormElement>) => {
			evt.preventDefault()

			// const { password, username } = this.state

			// if (!username) {
			// 	this.setState({ error: 'Username is required' })
			// 	return
			// }

			// if (!password) {
			// 	this.setState({ error: 'Password is required' })
			// 	return
			// }

			const authenticationPostOptions = {
				body: JSON.stringify({
					// password,
					// username
				}),
				cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
				credentials: 'same-origin', // include, same-origin, *omit
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				},
				method: 'POST'
			}

			dismissError()
			fetch('/api/auth', authenticationPostOptions as any)
				.then(response => response.json())
				.then(authenticationResult => {
					if (!authenticationResult) {
						return
					}
					if (!authenticationResult.success) {
						// this.setState({ error: 'Auth Failed' })
						return
					}
					// this.setState({ error: '', loginSuccess: true })
					// window.setTimeout(() => {
					// 	window.location.reload()
					// }, 2000)
				})
				.catch(error =>
					console.error(`Posting Authentication Error = ${error}`)
				)
		},
		handlePasswordChange: (evt: React.ChangeEvent<HTMLInputElement>) => {
			const newPassword = evt.target.value
			dispatch(actionCreators.setLoginPassword(newPassword))
		},
		handleUsernameChange: (evt: React.ChangeEvent<HTMLInputElement>) => {
			const newUsername = evt.target.value
			dispatch(actionCreators.setLoginUsername(newUsername))
		}
	}
}

const StatelessLogin = (props: any) => {
	return (
		<div className="login">
			<form onSubmit={props.handleFormSubmission}>
				<label>Username</label>
				<input
					type="text"
					value={props.username}
					onChange={props.handleUsernameChange}
				/>

				<label>Password</label>
				<input
					type="password"
					value={props.password}
					onChange={props.handlePasswordChange}
				/>

				<input className="login-button" type="submit" value="Login" />
			</form>
			{props.error && (
				<h3 onClick={props.dismissError} className="error">
					<button
						onClick={props.dismissError}
						className="dismiss-button"
					>
						❌
					</button>
					{props.error}
				</h3>
			)}
			{props.loginSuccess && (
				<h3 className="success">Success, redirecting...</h3>
			)}
		</div>
	)
}

const connectedLogin = connect(
	mapStateToProps,
	mapDispatchToProps
)(StatelessLogin)

export { connectedLogin as Login }
