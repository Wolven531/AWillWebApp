import * as React from 'react'
// import { connect } from 'react-redux'

class MonsterView extends React.Component<{ monster: any }> {
	public componentDidMount() {
		console.info('[MonsterView | componentDidMount]')
	}

	public render() {
		console.info('[MonsterView | render]')
		return (<div>
			<h3>Monster View</h3>
		</div>)
	}
}

export { MonsterView }
