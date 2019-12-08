Guide d'utilisation de l'application
  * User avec chien : 123(email) - 456(password) - 1234(credit card)
  * User sans chien : 456 - 789 - 1234
  * Un user avec DogId a -1 signifit qu'il ne possède aucun chien
  * Pour ajouter un chien au panier, il faut être connecté, il ne faut pas être propriétaire du chien ou l'avoir déjà dans son panier.
  
Ce qui doit être améliorer au niveau de la sécurité
  * Le HttpClient n'est pas utilisé, il a été essayé par les deux membres, mais cela n'a jamais fonctionné.
    Durant le debugger, l'exécuteur stopait rendu à cette méthode sans même expliquer pourquoi.
    HttpResponseMessage response = await _httpClient.GetAsync("https://dog.ceo/api/breeds/list%22);
    Certains forums mentionnaient l'utilisation d'un "deadlock", ce qui n'a jamais été vu et semblait ardu.

Aucun bug connus

Capture d'écran du profilage
