/// <reference types="Cypress" />

context('Basic application', () => {
	beforeEach(() => {
		cy.visit('/')
	})

	it('should load home page', () => {
		cy.url().should('eq', 'https://localhost:5001/')
		cy.title().should('eq', 'Index')
	})
})
