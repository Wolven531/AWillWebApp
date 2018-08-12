import * as React from 'react'

import './Login.css'

interface ILoginState {
	error: string
	password: string
	username: string
}

class Login extends React.Component<{}, ILoginState> {
	constructor(props: any) {
		super(props)
		this.state = {
			error: '',
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

					<input className="login-button" type="submit" value="Login" />
				</form>
				{this.state.error && <h3 onClick={this.dismissError} className="error">
					<button onClick={this.dismissError} className="dismiss-button">✖</button>
					{this.state.error}
				</h3>}
			</div>
		)
	}

	private dismissError = () => this.setState({ error: '' })

	private handleFormSubmission = (evt: React.FormEvent<HTMLFormElement>) => {
		evt.preventDefault()

		if (!this.state.username) {
			this.setState({ error: 'Username is required' })
			return
		}

		if (!this.state.password) {
			this.setState({ error: 'Password is required' })
			return
		}

		this.dismissError()
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
