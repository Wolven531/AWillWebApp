import * as React from 'react'

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

		this.dismissError()
		fetch('/api/auth', authenticationPostOptions as any)
			.then(response => response.json())
			.then(authenticationResult => {
				if (!authenticationResult) {
					return
				}
				if (!authenticationResult.success) {
					this.setState({ error: 'Auth Failed' })
					return
				}
				this.setState({ error: '', loginSuccess: true })
				window.setTimeout(() => {
					window.location.reload()
				}, 2000)
			})
			.catch(error => console.error(`Posting Authentication Error = ${error}`))
	}

	private handlePasswordChange = (evt: React.ChangeEvent<HTMLInputElement>) => {
		const newPassword = evt.target.value
		this.setState({
			password: newPassword
		})
	}

	private handleUsernameChange = (evt: React.ChangeEvent<HTMLInputElement>) => {
		const newUsername = evt.target.value
		this.setState({
			username: newUsername
		})
	}
}

export { Login }
