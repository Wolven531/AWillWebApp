import * as React from 'react'

import 'es6-promise'
import 'isomorphic-fetch'

import './MonsterSearcher.css'

interface IMonsterSearcherState {
	loading: boolean
	searchQuery: string
	searchResults: any[]
}

class MonsterSearcher extends React.Component<{}, IMonsterSearcherState> {
	constructor(props: any) {
		super(props)
		this.state = {
			loading: true,
			searchQuery: '',
			searchResults: []
		}
	}

	public render() {
		return (
			<div className="monster-searcher">
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
								onChange={this.handleSearchQueryUpdate}
							/>
						</label>
					</div>
					<button onClick={this.searchApiWithState}>Search</button>
					{this.state.searchResults.length > 0 && (
						<div className="search-results">
							{this.state.searchResults.map((searchResult: any, ind) => {
								return (
									<div key={ind} className="search-result">
										<div className="name-display">
											{searchResult.awakenedName} ({searchResult.name})
										</div>
										<img src={searchResult.image} className="normal-image" />
										<img src={searchResult.awakenedImage} className="awakened-image" />
									</div>
								)
							})}
						</div>
					)}
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

	private searchApi = (searchQuery: string) => {
		// if (!searchQuery) {
		// 	console.warn(`[searchApi] Unable to search API with searchQuery=${JSON.stringify(searchQuery)}`)
		// 	return
		// }
		fetch(`api/monsters/names/${searchQuery}`)
			.then(response => response.json())
			.then((searchResults: any[]) => this.setState({ searchQuery, searchResults }))
	}

	private searchApiWithState = () => this.searchApi(this.state.searchQuery)
}

export { IMonsterSearcherState, MonsterSearcher }
