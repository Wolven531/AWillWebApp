import * as React from 'react'
import { Monster } from '../../models/monster'
// import { connect } from 'react-redux'

class MonsterView extends React.Component<{ monster: Monster }> {
	public componentDidMount() {
		console.info('[MonsterView | componentDidMount]')
	}

	public render() {
		console.info('[MonsterView | render]')
		const { monster } = this.props
		return (
			<div>
				<h3>
					{monster.awakenedName} ({monster.element} {monster.name})
				</h3>
				<section>
					<p>Rating: {monster.rating} ⭐</p>
				</section>
				<section>
					<p>Early Runes: {monster.earlyRuneList || '-'}</p>
					<p>Early Rune Stats: {monster.earlyRuneValues || '-'}</p>
					<p>Late Runes: {monster.lateRuneList || '-'}</p>
					<p>Late Rune Stats: {monster.lateRuneValues || '-'}</p>
					<p>
						Stat Priority
						{monster.statPriority || '-'}
					</p>
				</section>
				<section>
					<img
						src={monster.image}
						alt={`Image of monster - ${monster.element} ${monster.name}`}
						title={`Image of monster - ${monster.element} ${monster.name}`}
					/>
					<img
						src={monster.awakenedImage}
						alt={`Image of awakened monster - ${monster.awakenedName}`}
						title={`Image of awakened monster - ${monster.awakenedName}`}
					/>
				</section>
			</div>
		)
	}
}

export { MonsterView }
