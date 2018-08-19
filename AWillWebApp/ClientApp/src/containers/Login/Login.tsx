import * as React from 'react'
import { connect } from 'react-redux'

import { userActions, userConstants } from '../LoginPage'

import './Login.css'

interface ILoginState {
	error: string
	loginSuccess: boolean
	password: string
	username: string
}

class Login extends React.Component<{}, ILoginState> {
	constructor(props: any) {
		super(props)
		this.state = {
			error: '',
			loginSuccess: false,
			password: '',
			username: ''
		}
	}

	public componentDidMount() {
		(this.props as any).dispatch(userActions.logout())
		// NOTE: to refresh the page during CSS styling without HMR
		// window.setTimeout(() => window.location.reload(), 2500)
	}

	public render() {
		return (
			<div className="login">
				<form onSubmit={this.handleFormSubmission}>
					<label>Username</label>
					<input
						type="text"
						value={this.state.username}
						onChange={this.handleUsernameChange}
					/>

					<label>Password</label>
					<input
						type="password"
						value={this.state.password}
						onChange={this.handlePasswordChange}
					/>

					<input
						className="login-button"
						type="submit"
						value="Login"
					/>
				</form>
				{this.state.error && (
					<h3 onClick={this.dismissError} className="error">
						<button
							onClick={this.dismissError}
							className="dismiss-button"
						>
							❌
						</button>
						{this.state.error}
					</h3>
				)}
				{this.state.loginSuccess && (
					<h3 className="success">Success, redirecting...</h3>
				)}
			</div>
		)
	}

	private dismissError = () => this.setState({ error: '' })

	private handleFormSubmission = (evt: React.FormEvent<HTMLFormElement>) => {
		evt.preventDefault()

		const { password, username } = this.state

		if (!username) {
			this.setState({ error: 'Username is required' })
			return
		}

		if (!password) {
			this.setState({ error: 'Password is required' })
			return
		}

		this.dismissError()
		userActions
			.login(username, password)(
				(this.props as any).dispatch
			)
			.then(
				() => {
					console.log('resolve')
				},
				(wasAuthFailure: boolean) => {
					console.log(`reject, wasAuthFailure=${wasAuthFailure}`)
					window.location.reload()
				}
			)
	}

	private handlePasswordChange = (
		evt: React.ChangeEvent<HTMLInputElement>
	) => {
		const newPassword = evt.target.value
		this.setState({
			password: newPassword
		})
	}

	private handleUsernameChange = (
		evt: React.ChangeEvent<HTMLInputElement>
	) => {
		const newUsername = evt.target.value
		this.setState({
			username: newUsername
		})
	}
}

const mapStateToProps = (state: any) => {
	const { authentication, history } = state
	const { loggingIn } = authentication
	return {
		history,
		loggingIn
	}
}

const connectedLogin = connect(mapStateToProps)(Login)

export { connectedLogin as Login }
