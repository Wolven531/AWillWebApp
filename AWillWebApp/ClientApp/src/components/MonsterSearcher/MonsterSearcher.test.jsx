import * as React from 'react'
import { configure, Enzyme, shallow } from 'enzyme'
import Adapter from 'enzyme-adapter-react-16'
import { MonsterSearcher } from './MonsterSearcher'

configure({ adapter: new Adapter() })

Enzyme.configure({
	adapter: new Adapter()
})

// const sum = (a, b) => a + b

// describe('sum function', () => {
// 	test('adds 1 + 2 to equal 3', () => {
// 		expect(sum(1, 2)).toBe(3)
// 	})
// })

describe('MonsterSearcher', () => {
	test('qwer', () => {
		const fixture = shallow(<MonsterSearcher />)
		expect(1 + 2).toBe(3)
	})
})
