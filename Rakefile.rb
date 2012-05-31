require 'rake'
require 'albacore'

task :default => [:build]

desc "Build"
msbuild :build do |msb|
  msb.properties :configuration => :Release
  msb.targets :Clean, :Build
  msb.solution = "headlock.sln"
end