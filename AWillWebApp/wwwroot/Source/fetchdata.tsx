import * as React from 'react'
import 'es6-promise'
import 'isomorphic-fetch'

interface IFetchDataState {
	apiDataObjects: object[]
	loading: boolean
}

export default class FetchData extends React.Component<null, IFetchDataState> {
	constructor(props) {
		super(props)
		this.state = {
			apiDataObjects: [],
			loading: true
		}
		this.refreshData()
	}

	public render() {
		const contents = this.state.loading ?
			(<p><em>Loading...</em></p>)
			: (<textarea>{JSON.stringify(this.state.apiDataObjects, null, 4)}</textarea>)

		return (
			<div>
				<h1>API Check</h1>
				<p>This component demonstrates fetching data from the server</p>
				<button onClick={() => this.refreshData}>Refresh</button>
				{contents}
			</div>
		)
	}

	private refreshData = () => {
		fetch('api/SampleDummyData/')
			.then(response => response.json())
			.then(apiDataObjects => {
				this.setState({
					apiDataObjects,
					loading: false
				})
			})
	}
}