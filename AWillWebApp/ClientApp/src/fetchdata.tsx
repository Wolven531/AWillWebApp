import * as React from 'react'
import 'es6-promise'
import 'isomorphic-fetch'

export interface IFetchDataState {
	loading: boolean
	monsters: object[]
	searchQuery: string
	searchResults: string[]
}

export default class FetchData extends React.Component<{}, IFetchDataState> {
	constructor(props: any) {
		super(props)
		this.state = {
			loading: true,
			monsters: [],
			searchQuery: '',
			searchResults: []
		}
		this.refreshData()
	}

	public render() {
		return (
			<div className="fetchdata">
				<h1>API</h1>
				<div>
					<h2>IMonsterRepository.GetMonsterNames()</h2>
					<div>
						<h3>Parameters</h3>
						<label htmlFor="search-query">
							<h4>Search Query (string)</h4>
							<input
								type="text"
								id="search-query"
								name="search-query"
								value={this.state.searchQuery}
								onChange={this.handleSearchQueryUpdate} />
						</label>
					</div>
					<button onClick={this.searchApiWithState}>Search</button>
					{this.state.searchResults.length > 0 &&
						<textarea
							cols={20}
							readOnly={true}
							rows={20}
							value={JSON.stringify(this.state.searchResults, null, 4)}>
						</textarea>
					}
				</div>
			</div>
		)
	}

	private handleSearchQueryUpdate = (evt: React.ChangeEvent<HTMLInputElement>) => {
		if (!evt || !evt.target) {
			return
		}
		const searchQuery = String(evt.target.value)
		this.searchApi(searchQuery)
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

	private searchApi = (searchQuery: string) => {
		if (!searchQuery) {
			console.warn(`[searchApi] Unable to search API with searchQuery=${JSON.stringify(searchQuery)}`)
			return
		}
		fetch(`api/monsters/names/${searchQuery}`)
			.then(response => response.json())
			.then((searchResults: string[]) => {
				console.log(`[searchApi] Got search results for searchQuery=${JSON.stringify(searchQuery)}, number searchResults=${searchResults.length}`)
				this.setState({ searchQuery, searchResults })
			})
	}

	private searchApiWithState = () => this.searchApi(this.state.searchQuery)
}
