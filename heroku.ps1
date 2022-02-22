heroku container:login

cd .\MarxPrime.App\
heroku container:push worker -a marx-prime

heroku container:release worker -a marx-prime

cd ..