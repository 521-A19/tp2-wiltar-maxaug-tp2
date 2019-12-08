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

Particularités de configuration de l'environnement de développement
  * Dans l'app.cs, le user avec chien 123/456 ajoute automatiquement le dog1 (id = 1)
    Nous utilisons dogsRepository.DeleteAll() dans l'app, pour tout supprimer à chaque fois.

Aucun bug connus

Capture d'écran du profilage
https://cdn.discordapp.com/attachments/643535598652489739/653341195627397150/Profiler.PNG