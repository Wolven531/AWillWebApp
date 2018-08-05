/// <reference types="Cypress" />

// TODO: import Monster fixture file
// const monsters = cy.fixture()
const TOTAL_MONSTERS = 495
const TYPING_DELAY = 300

context('Home page', () => {
	beforeEach(() => {
		cy.visit('/')
	})

	it('should load home page', () => {
		cy.url().should('eq', 'https://localhost:5001/')
		cy.title().should('eq', 'Home')
		cy.get('#app > .monster-searcher:first-child h1').contains(/^API$/)
	})
	
	it('should load empty search box, no search results', () => {
		cy.get('input[name="search-query"]').should('have.value', '')
		cy.get('.search-results').should('not.exist')
		cy.get('.search-result').should('not.exist')
	})
	
	context('typing letter in search box', () => {
		beforeEach(() => {
			cy.get('input[name="search-query"]').clear().type('c')
		})
		
		it('should show search results', () => {
			cy.get('.search-results').should('exist')
			cy.get('.search-result').find('div').should('have.length', 165)
		})

		context('clearing search box via backspace', () => {
			beforeEach(() => {
				cy.get('input[name="search-query"]').type('{backspace}')
			})
			
			it('should clear search box, still show results', () => {
				cy.get('input[name="search-query"]').should('have.value', '')
				cy.get('.search-results').should('exist')
				cy.get('.search-result').find('div').should('have.length', TOTAL_MONSTERS)
			})
		})
	})
	
	context('typing exact match in search box', () => {
		beforeEach(() => {
			cy.get('input[name="search-query"]').clear().type('chasun', { delay: TYPING_DELAY })
		})
		
		it('should show exact results', () => {
			cy.get('.search-results').should('exist')
			cy.get('.search-result').find('div').should('have.length', 1)
			cy.get('.search-result .name-display').contains(/^Chasun \(Wind Sky Dancer\)$/)
			cy.get('.search-result img.normal-image').should('have.attr', 'src', 'https://summonerswarskyarena.info/wp-content/uploads/2015/01/sky-dancer-wind.png')
			cy.get('.search-result img.awakened-image').should('have.attr', 'src', 'https://summonerswarskyarena.info/wp-content/uploads/2015/01/chasun.png')
		})
	})
	
	context('typing non-match in search box', () => {
		beforeEach(() => {
			cy.get('input[name="search-query"]').clear().type('qwer', { delay: TYPING_DELAY })
		})
		
		it('should show no results', () => {
			cy.get('.search-results').should('not.exist')
			cy.get('.search-result').should('not.exist')
		})
	})
})
