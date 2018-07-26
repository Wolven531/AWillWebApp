import * as React from 'react'
import * as ReactDOM from 'react-dom'

export interface ICounterState {
	count: number
}

export default class Counter extends React.Component<null, ICounterState> {
	constructor(props: any) {
		super(props)
		this.state = {
			count: 0
		}
	}

	public render() {
		return (
			<div>
				<h1>Counter</h1>
				<p>This is a simple example of a React component</p>
				<p>
					Current count: <strong>{this.state.count}</strong>
				</p>
				<button onClick={this.incrementCount}>
					Increment
				</button>
			</div>
		)
	}

	private incrementCount = () => this.setState({ count: this.state.count + 1 })
}
