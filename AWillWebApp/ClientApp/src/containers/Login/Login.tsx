import { createBrowserHistory, History } from 'history'
import * as React from 'react'
import { connect } from 'react-redux'

import { userActions, userConstants } from '../UserActions'

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
		const dispatch = (this.props as any).dispatch

		userActions
			.login(username, password)(dispatch)
			.then(
				(success: boolean) => {
					if (!success) {
						this.setState({ error: 'Auth Failed' })
						return
					}
					const hist: History = (this.props as any).history
					const unlisten = hist.listen((location, action) => {
						// NOTE: location is an object like window.location
						console.log(action, location.pathname, location.state)
						window.location.assign(location.pathname)
					})
					hist.push('/')
				},
				(error: any) => {
					console.log(`[reject | handleFormSubmission | Login] Error=${error}`)
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
	const { authentication, routing } = state
	const { loggingIn } = authentication
	const hist = createBrowserHistory()

	// TODO: work on routing?
	if (!routing.location) {
		routing.location = hist.location
	}

	// TODO: figure out why history is not passed in
	return {
		history: hist || history,
		loggingIn,
		routing
	}
}

const connectedLogin = connect(mapStateToProps)(Login)

export { connectedLogin as Login }
