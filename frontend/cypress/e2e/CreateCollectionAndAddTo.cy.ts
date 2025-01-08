describe('Create a collection, add a post to it, remove it and delete the collection', () => {
    
    const email = 'example@example.com';
    const password = 'password';  
    const collectionName  = 'Test Collection';
    const existingPostTitle  = 'Harry Potter';    

    before(() =>{
        cy.visit('http://localhost:3000/login')
        cy.get('input[id="email"]').type(email) 
        cy.get('input[id="password"]').type(password)
        cy.get('button[type="submit"]').click();
    })
  
    it('should create a collection', () => {
      
      //Navigate to the create collection page  
      cy.visit('http://localhost:3000/collections')
      
      //Create Collection
      cy.get('input[placeholder="New collection name"]').type(collectionName);
      cy.get('button').contains('Create Collection').click();
      
      // Verify that the collection is created
      cy.contains(collectionName).should('be.visible');
      
      //Go back to home page
      cy.visit('http://localhost:3000/home')

      //Add Post to new Collection
      cy.get('button').contains('Add to Collection').click();
      cy.get('button').contains(collectionName).click();

      //Verify that the post is added to the collection
      cy.visit('http://localhost:3000/collections')
      cy.get('button').contains('View').click();
      cy.contains(existingPostTitle).should('be.visible');

      //Delete the post from the collection
      cy.get('button').contains('Delete').click();

      //Verify the post is deleted from the collection
      cy.reload();
      cy.contains(existingPostTitle).should('not.exist'); 

      //Delete the collection
      cy.visit('http://localhost:3000/collections')
      cy.get('button').contains('Delete').click();

      //Verify the collection is deleted
      cy.contains(collectionName).should('not.exist');
    });
  });
  