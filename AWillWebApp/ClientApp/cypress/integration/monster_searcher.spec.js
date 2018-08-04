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
		cy.get('textarea[name="search-results"]').should('exist')
	})
	
	context('typing letter in search box', () => {
		beforeEach(() => {
			cy.get('input[name="search-query"]').clear().type('c')
		})
		
		it('should show search results', () => {
			cy.get('textarea[name="search-results"]').should('exist')
		})

		context('clearing search box via backspace', () => {
			beforeEach(() => {
				cy.get('input[name="search-query"]').type('{backspace}')
			})
			
			it('should clear search box, still show results', () => {
				cy.get('input[name="search-query"]').should('have.value', '')
				cy.get('textarea[name="search-results"]').should('exist')
			})
		})
	})
	
	context('typing exact match in search box', () => {
		beforeEach(() => {
			cy.get('input[name="search-query"]').clear().type('chasun', { delay: 200 })
		})
		
		it('should show exact results', () => {
			cy.get('textarea[name="search-results"]').should('exist')
			cy.get('textarea[name="search-results"]')
				.should('have.value', JSON.stringify(['Wind Sky Dancer', 'Chasun'], null, 4))
		})
	})
	
	context('typing non-match in search box', () => {
		beforeEach(() => {
			cy.get('input[name="search-query"]').clear().type('qwer', { delay: 200 })
		})
		
		it('should show no results', () => {
			cy.get('textarea[name="search-results"]').should('exist')
			cy.get('textarea[name="search-results"]').should('have.value', JSON.stringify([], null, 4))
		})
	})
})
