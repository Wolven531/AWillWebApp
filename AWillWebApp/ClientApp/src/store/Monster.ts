import { Monster } from '../models/monster'

const REQUEST_MONSTERS = 'request_monsters'
const RECEIVE_MONSTERS = 'receive_monsters'
const REQUEST_SINGLE_MONSTER = 'request_single_monster'
const RECEIVE_SINGLE_MONSTER = 'receive_single_monster'

/*
	This interface is used to represent the action this reducer is capable
	of processing
*/
interface IMonsterReducerAction {
	payload: any
	type: string
}

/*
	This interface is used to represent the state maintained by this reducer
*/
interface IMonsterState {
	monsters: Monster[]
}

/*
	This is the initial state for this reducer
*/
const initialState: IMonsterState = {
	monsters: []
}

/*
	This is a map of functions that create actions which may be processed by
	this reducer
*/
const actionCreators = {
	loadMonsterWithImages: (monsterId: string) => async (dispatch: (action: IMonsterReducerAction) => void) => {
		dispatch({ type: REQUEST_SINGLE_MONSTER, payload: { monsterId } })
		const response = await fetch(`api/monsters/${monsterId}`)
		const monsterWithImage: Monster = await response.json()
		dispatch({ type: RECEIVE_SINGLE_MONSTER, payload: { monsterWithImage } })
	},
	loadMonstersFromApi: () => async (dispatch: (action: IMonsterReducerAction) => void) => {
		dispatch({ type: REQUEST_MONSTERS, payload: {} })
		const response = await fetch('api/monsters')
		const monsters: Monster[] = await response.json()
		dispatch({ type: RECEIVE_MONSTERS, payload: { monsters } })
	}
}

const reducer = (state: Partial<IMonsterState> = initialState, action: IMonsterReducerAction) => {
	const { payload, type } = action

	if (type === RECEIVE_MONSTERS) {
		return {
			...state,
			monsters: payload.monsters
		}
	}

	if (type === RECEIVE_SINGLE_MONSTER) {
		return {
			...state,
			monsterWithImage: payload.monsterWithImage
		}
	}

	return state
}

export { actionCreators, IMonsterReducerAction, reducer }
