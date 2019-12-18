//
// Copyright (C) 2016 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#import "ZPLADObjectCache.h"

@implementation ZPLADObjectCache

+ (instancetype)sharedInstance {
    static ZPLADObjectCache *sharedInstance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (id)init {
    self = [super init];
    if(self){
        _references = [[NSMutableDictionary alloc] init];
    }
    return self;
}

@end

@implementation NSObject (ZPLADOwnershipAdditions)

- (NSString *)zplad_referenceKey {
    return [NSString stringWithFormat:@"%p", (void *)self];
}

@end
