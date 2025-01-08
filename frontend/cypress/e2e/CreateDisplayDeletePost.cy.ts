
/// <reference types="cypress" />

describe('Create, display and delete a new post', () => {

  const email = 'example@example.com';
  const password = 'password';  
  const postTitle  = 'Title Test';	
  const postReview = 'Review Test';  

  before(() =>{
    cy.visit('http://localhost:3000/login')
    cy.get('input[id="email"]').type(email) 
    cy.get('input[id="password"]').type(password)
    cy.get('button[type="submit"]').click();
  })

  it('Create, display and delete a new post', () => {

    //Create a new post
    cy.get('input[placeholder="Title"]').type(postTitle);
    cy.get('textarea[placeholder="Review"]').type(postReview);
    cy.get('.star').eq(2).click();
    cy.get('button').contains('Submit').click();

    //Check if the post is displayed
    cy.contains(postTitle).should('be.visible');
    cy.contains(postReview).should('be.visible');

    //Delete the post
    cy.contains(postTitle)
    .parent() 
    .find('button').contains('Delete') 
    .click(); 

    // Verify that the post is deleted
    cy.contains(postTitle).should('not.exist');
    cy.contains(postReview).should('not.exist');
      
  })
})