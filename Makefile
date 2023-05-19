.DEFAULT_GOAL := all
build:
	dotnet publish -c PublishRelease -r $(target)
	mv ./bin/PublishRelease/net7.0/$(target)/publish ./release/$(target)

clean:
	rm -rf bin/PublishRelease/net7.0/
	rm -rf release
	mkdir release

all:
	make clean
	make build target=osx-arm64
	make build target=linux-x64
	make build target=win-x64
