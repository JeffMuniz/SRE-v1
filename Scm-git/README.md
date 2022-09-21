##   Repository about Repository



git checkout --orphan latest_branch
git add -A
git commit -am "Bhrought to by MacStevie Cleaners"
git branch -D main
git branch -m main
git push -f origin main
git push origin -u main:main

##git push origin -u local_branch:remote_branch
