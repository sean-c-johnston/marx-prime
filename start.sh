#!/bin/bash

if [ -s .env ]; then
	docker run -d --restart unless-stopped marx-prime
else
	echo "put something in your .env file you dingus"
fi
