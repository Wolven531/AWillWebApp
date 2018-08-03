/// <reference types="Cypress" />

context('Home page', () => {
	beforeEach(() => {
		cy.visit('/')
	})

	it('should load home page', () => {
		cy.url().should('eq', 'https://localhost:5001/')
		cy.title().should('eq', 'Home')
		cy.get('#app > .fetchdata:first-child h1').contains(/^API$/)
	})
	
	it('should load empty search box, no search results', () => {
		cy.get('input[name="search-query"]').should('have.value', '')
		cy.get('textarea[name="search-results"]').should('not.exist')
	})
	
	context('typing in the search box', () => {
		beforeEach(() => {
			cy.get('input[name="search-query"]').type('c')
		})
		
		it('should show search results', () => {
			cy.get('textarea[name="search-results"]').should('exist')
		})
	})
})
