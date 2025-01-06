
/// <reference types="cypress" />

describe('template spec', () => {
  it('passes', () => {
    cy.visit('http://localhost:3000/login')
    cy.get('input[id="email"]').should('be.visible'); // Wacht totdat het input-element zichtbaar is
    cy.get('input[id="email"]').type('example@example.com') 
    cy.get('input[id="password"]').type('password')
    cy.get('button[type="submit"]').click(); // Selecteer via type


    // Wacht tot de homepage wordt geladen
    cy.url().should('include', '/Home') // Verifieer dat de URL de homepagina is
  })
})