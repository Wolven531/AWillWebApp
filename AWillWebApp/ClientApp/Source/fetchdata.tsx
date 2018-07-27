import * as React from 'react'
import 'es6-promise'
import 'isomorphic-fetch'

export interface IFetchDataState {
	loading: boolean
	monsters: object[]
}

export default class FetchData extends React.Component<{}, IFetchDataState> {
	constructor(props: any) {
		super(props)
		this.state = {
			loading: true,
			monsters: []
		}
		this.refreshData()
	}

	public render() {
		const contents = this.state.loading ?
			(<p><em>Loading...</em></p>)
			: (<textarea>{JSON.stringify(this.state.monsters, null, 4)}</textarea>)

		return (
			<div>
				<h1>API Check</h1>
				<p>This component demonstrates fetching data from the server</p>
				<button onClick={() => this.refreshData}>
					Refresh
				</button>
				{contents}
			</div>
		)
	}

	private refreshData = () => {
		fetch('api/monsters/')
			.then(response => response.json())
			.then(monsters => {
				this.setState({
					loading: false,
					monsters
				})
			})
	}
}